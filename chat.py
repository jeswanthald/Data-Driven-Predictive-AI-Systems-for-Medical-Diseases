from flask import Blueprint, render_template, request, redirect, url_for, session, jsonify
from . import db
from .models import ChatMessage
import os
import socket
import base64
import uuid
from datetime import datetime
from werkzeug.utils import secure_filename

chat = Blueprint('chat', __name__)
UPLOAD_FOLDER = 'website/static/uploads'

# ✅ Ensure upload folder exists
if not os.path.exists(UPLOAD_FOLDER):
    os.makedirs(UPLOAD_FOLDER)


# ✅ Send message to Windows Forms app via TCP
def send_to_windows_form(email, message, file_path=None):
    HOST = '127.0.0.1'
    PORT = 9000

    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect((HOST, PORT))

            file_url = ""
            if file_path:
                file_url = f"http://127.0.0.1:5000/static/{file_path.split('static/')[-1]}"

            name = session.get('user_name', 'Unknown')
            email = email or session.get('user_email', 'unknown@email.com')
            payload = f"{name}|||{email}|||{message}|||{file_url}"

            print("✅ Sending to Windows Forms:", payload)
            s.sendall(payload.encode('utf-8'))

    except Exception as e:
        print("⚠️ Could not send to Windows Forms:", e)


# ✅ Chat Page Route (GET + POST)
@chat.route('/chat', methods=['GET', 'POST'])
def chatbox():
    if request.method == 'POST':
        email = session.get('user_email')
        name = session.get('user_name')
        message = request.form.get('message', '').strip()
        file = request.files.get('file')
        audio_data = request.form.get('audio_data')
        file_path = None

        # ✅ Handle file upload
        if file and file.filename:
            filename = f"{uuid.uuid4().hex[:8]}_{secure_filename(file.filename)}"
            full_path = os.path.join(UPLOAD_FOLDER, filename)
            file.save(full_path)
            file_path = full_path.replace("website/", "")

        # ✅ Handle audio recording
        if audio_data:
            try:
                header, encoded = audio_data.split(",", 1)
                audio_bytes = base64.b64decode(encoded)

                audio_filename = f"audio_{datetime.now().strftime('%Y%m%d%H%M%S')}.webm"
                audio_full_path = os.path.join(UPLOAD_FOLDER, audio_filename)

                with open(audio_full_path, "wb") as f:
                    f.write(audio_bytes)

                file_path = audio_full_path.replace("website/", "")
            except Exception as e:
                print("❌ Error saving audio:", e)

        # ✅ Ensure something is submitted
        if not message and not file_path:
            return "Please enter a message or upload a file/audio", 400

        # ✅ Save to database
        chat_msg = ChatMessage(
            sender_email=email,
            message=message,
            file_path=file_path
        )
        db.session.add(chat_msg)
        db.session.commit()

        # ✅ Notify Windows app
        send_to_windows_form(email, message, file_path)

        return redirect(url_for('chat.chatbox'))

    # ✅ GET request — show chat messages for the logged-in user only
    user_email = session.get('user_email')
    if not user_email:
        return redirect(url_for('auth.login'))  # Redirect if not logged in

    messages = ChatMessage.query.filter_by(sender_email=user_email) \
                .order_by(ChatMessage.timestamp.desc()).limit(50).all()
    return render_template("chat.html", messages=messages)


# ✅ API to receive reply (text + file/audio) from Windows Forms
@chat.route('/api/reply', methods=['POST'])
def api_reply():
    if request.content_type.startswith('multipart/form-data'):
        email = request.form.get('email')
        reply = request.form.get('reply')
        file = request.files.get('file')
    else:
        data = request.get_json()
        email = data.get('email')
        reply = data.get('reply')
        file = None

    if not email or not reply:
        return jsonify({'error': 'Missing email or reply'}), 400

    # ✅ Get latest message from that user
    message = ChatMessage.query.filter_by(sender_email=email) \
                .order_by(ChatMessage.timestamp.desc()).first()
    if not message:
        return jsonify({'error': 'No chat message found for this email'}), 404

    # ✅ Save text reply
    message.reply = reply

    # ✅ Save file/audio reply (optional)
    if file and file.filename:
        filename = f"reply_{uuid.uuid4().hex[:8]}_{secure_filename(file.filename)}"
        full_path = os.path.join(UPLOAD_FOLDER, filename)
        file.save(full_path)
        message.reply_file_path = full_path.replace("website/", "")

    db.session.commit()

    return jsonify({'message': 'Reply saved successfully'}), 200
