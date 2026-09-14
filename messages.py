from flask import Blueprint, render_template, request, redirect, url_for
from .models import Messages
from . import db
import requests

messages = Blueprint('messages', __name__)

@messages.route("/msg", methods=['GET', 'POST'])
def msg():
    if request.method == 'POST':
        name = request.form.get('name')
        email = request.form.get('email')
        message = request.form.get('message')

        # Save to database
        new_message = Messages(name=name, email=email, messages=message)
        db.session.add(new_message)
        db.session.commit()

        # Send data to Windows Forms HTTP listener
        try:
            url = 'http://127.0.0.1:5001/receive_message'  # Replace with actual Windows listener URL
            payload = {
                'name': name,
                'email': email,
                'message': message
            }
            response = requests.post(url, json=payload)
            print("Response from Windows:", response.status_code)
        except Exception as e:
            print("HTTP send error:", e)

        return render_template('receive.html')  # Show thank-you page
    else:
        return render_template('base.html')
