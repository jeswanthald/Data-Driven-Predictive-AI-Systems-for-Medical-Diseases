from flask import Blueprint, render_template, request, flash, redirect, url_for, session
from werkzeug.security import generate_password_hash, check_password_hash
from .models import User
from . import db
from flask_login import login_user, login_required, logout_user, current_user

auth = Blueprint('auth', __name__)

@auth.route('/signup', methods=['GET', 'POST'])
def signup():
    if request.method == 'POST':
        name = request.form.get('name')
        email = request.form.get('email')
        password1 = request.form.get('password1')
        password2 = request.form.get('password2')

        if not name or not email or not password1 or not password2:
            flash("Please fill in all fields", category='error')
        elif password1 != password2:
            flash("Passwords don't match", category='error')
        elif len(password1) < 6:
            flash("Password must be at least 6 characters", category='error')
        else:
            # Check if user already exists
            user_exists = User.query.filter_by(email=email).first()
            if user_exists:
                flash("Email already exists. Please login.", category='error')
                return redirect(url_for('auth.login'))

            # Create new user
            new_user = User(name=name, email=email, password=generate_password_hash(password1, method='sha256'))
            db.session.add(new_user)
            db.session.commit()

            flash("Signup successful! Please log in.", category='success')
            return redirect(url_for('auth.login'))

    return render_template('signup.html')


@auth.route('/login', methods=['GET', 'POST'])
def login():
    if request.method == 'POST':
        email = request.form.get('email')
        password = request.form.get('password')

        user = User.query.filter_by(email=email).first()

        if not user:
            flash('No account found with this email.', category='error')
        elif not check_password_hash(user.password, password):
            flash('Incorrect password.', category='error')
        else:
            session['user_email'] = user.email
            session['user_name'] = user.name
            flash('Login successful!', category='success')
            return redirect(url_for('chat.chatbox'))  # Redirect to chat after login

    return render_template('login.html')


@auth.route('/logout')
def logout():
    session.clear()
    flash('You have been logged out.', category='success')
    return redirect(url_for('auth.login'))
