from flask import Blueprint, jsonify
from .models import ChatMessage

messages_api = Blueprint('messages_api', __name__)

@messages_api.route('/api/inbox', methods=['GET'])
def get_inbox_messages():
    messages = ChatMessage.query.order_by(ChatMessage.timestamp.desc()).limit(50).all()

    result = []
    for msg in messages:
        result.append({
            "name": msg.sender.name if msg.sender else msg.sender_email.split('@')[0],  # ✅ updated from msg.user to msg.sender
            "email": msg.sender_email,
            "message": msg.message,
            "fileUrl": msg.file_path,
            "timestamp": msg.timestamp.strftime("%Y-%m-%d %H:%M:%S") if msg.timestamp else ""
        })

    return jsonify(result)
