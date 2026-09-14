from . import db
from sqlalchemy.sql import func
from flask_login import UserMixin

# ✅ User model
class User(db.Model, UserMixin):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(150), nullable=False)
    email = db.Column(db.String(150), unique=True, nullable=False)
    password = db.Column(db.String(256), nullable=False)

    # 🔗 Defines a one-to-many relationship with ChatMessage
    chat_messages = db.relationship('ChatMessage', backref='sender', lazy=True)  # ✅ renamed to avoid conflict


# ✅ Optional general messages model
class Messages(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    date = db.Column(db.DateTime(timezone=True), default=func.now())
    name = db.Column(db.String(150))
    email = db.Column(db.String(150))
    messages = db.Column(db.String(10000))


# ✅ ChatMessage model for storing messages and replies
class ChatMessage(db.Model):
    __tablename__ = 'chat_message'

    id = db.Column(db.Integer, primary_key=True)
    sender_email = db.Column(db.String(150), nullable=False)
    message = db.Column(db.Text, nullable=True)
    file_path = db.Column(db.String(500), nullable=True)
    timestamp = db.Column(db.DateTime(timezone=True), server_default=func.now())
    reply = db.Column(db.Text, nullable=True)
    reply_file_path = db.Column(db.String(500), nullable=True)

    user_id = db.Column(db.Integer, db.ForeignKey('user.id'))  # ✅ foreign key (creates sender via backref)
