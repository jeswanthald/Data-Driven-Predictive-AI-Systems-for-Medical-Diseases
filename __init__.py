from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from flask_login import LoginManager
import os
import random

db = SQLAlchemy()
DB_NAME = 'database.db'

def create_app():
    app = Flask(__name__)
    random.seed(0)

    # App config
    app.config['SECRET_KEY'] = os.urandom(24)
    app.config['SQLALCHEMY_DATABASE_URI'] = f'sqlite:///{DB_NAME}'
    app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = True

    db.init_app(app)

    # Flask-Login setup
    login_manager = LoginManager()
    login_manager.login_view = 'auth.login'
    login_manager.init_app(app)

    from .models import User

    @login_manager.user_loader
    def load_user(user_id):
        return User.query.get(int(user_id))

    # Register blueprints
    from .views import views
    from .prediction import prediction
    from .messages import messages
    from .vitamin.routes import vitamin_bp
    from .auth import auth
    from .chat import chat
    from .messages_api import messages_api  # ✅ NEW API ENDPOINT

    app.register_blueprint(auth, url_prefix='/')
    app.register_blueprint(views, url_prefix='/')
    app.register_blueprint(prediction, url_prefix='/')
    app.register_blueprint(messages, url_prefix='/')
    app.register_blueprint(vitamin_bp, url_prefix='/vitamin/')
    app.register_blueprint(chat, url_prefix='/')
    app.register_blueprint(messages_api, url_prefix='/')  # ✅ REGISTER API

    # Create database if it doesn't exist
    create_database(app)

    print("🧭 ROUTES:", app.url_map)

    return app

def create_database(app):
    if not os.path.exists('website/' + DB_NAME):
        with app.app_context():
            db.create_all()
            print("✅ Database created!")
