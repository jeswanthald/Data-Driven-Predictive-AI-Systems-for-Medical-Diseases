from flask import Blueprint, render_template

views = Blueprint('views', __name__)

@views.route("/")
def home():
    return render_template('base.html')

@views.route("/kidney")
def kidney():
    return render_template('kidney_index.html')

@views.route("/kidney_form")
def kidney_form():
    return render_template('kidney.html')

@views.route("/liver")
def liver():
    return render_template('liver_index.html')

@views.route("/liver_form")
def liver_form():
    return render_template('liver.html')

@views.route("/heart")
def heart():
    return render_template('heart_index.html')

@views.route("/heart_form")
def heart_form():
    return render_template('heart.html')

@views.route("/stroke")
def stroke():
    return render_template('stroke_index.html')

@views.route("/stroke_form")
def stroke_form():
    return render_template('stroke.html')

@views.route("/diabete")
def diabete():
    return render_template('diabete_index.html')

@views.route("/diabete_form")
def diabete_form():
    return render_template('diabete.html')

@views.route("/pneumonia")
def pneumonia():
    return render_template('pneumonia_index.html')

@views.route("/pneumonia_form")
def pneumonia_form():
    return render_template('pneumonia.html')

@views.route("/see")
def see():
    return render_template('see.html')
