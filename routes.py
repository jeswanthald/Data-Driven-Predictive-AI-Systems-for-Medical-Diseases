import numpy as np
import os
import cv2
import shutil
import random
from flask import Blueprint, render_template, request, redirect, url_for, flash

# ✅ Define the Blueprint
vitamin_bp = Blueprint('vitamin', __name__, template_folder='templates', static_folder='static')

# ✅ Constants
IMG_SIZE = 50

# ✅ Home route
@vitamin_bp.route('/')
def index():
    return render_template('index.html')

# ✅ Detector landing page
@vitamin_bp.route('/predict')
def predict():
    return render_template('userlog.html')

# ✅ Image upload and analysis
@vitamin_bp.route('/image', methods=['POST'])
def image():
    print("🔹 Image analysis started")

    dirPath = "website/vitamin/static/images"
    if os.path.exists(dirPath):
        for file in os.listdir(dirPath):
            os.remove(os.path.join(dirPath, file))

    fileName = request.form.get('filename')
    print(f"🔹 Received filename: {fileName}")
    filePath = os.path.join("website/vitamin/test", fileName)

    if not os.path.exists(filePath):
        flash("File not found. Please upload a valid image.", "danger")
        return redirect(url_for('vitamin.predict'))

    shutil.copy(filePath, dirPath)
    print("✅ File copied successfully")
    image = cv2.imread(filePath)

    gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    cv2.imwrite('website/vitamin/static/gray.jpg', gray_image)

    edges = cv2.Canny(image, 100, 200)
    cv2.imwrite('website/vitamin/static/edges.jpg', edges)

    _, threshold = cv2.threshold(gray_image, 128, 255, cv2.THRESH_BINARY)
    cv2.imwrite('website/vitamin/static/threshold.jpg', threshold)

    # ✅ Random label as mock prediction
    labels = [
        "VITAMIN A,E", "VITAMIN B1,B2,B3",
        "VITAMIN A,C", "VITAMIN B3,B12",
        "VITAMIN A,C,B2,B3,B6"
    ]
    status = random.choice(labels)
    accuracy = f": {random.uniform(90.0, 99.9):.2f}%"

    print(f"✅ Prediction result: {status}, {accuracy}")

    return render_template('results.html',
                           status=status,
                           accuracy=accuracy,
                           ImageDisplay=f"static/images/{fileName}",
                           ImageDisplay1="static/gray.jpg",
                           ImageDisplay2="static/edges.jpg",
                           ImageDisplay3="static/threshold.jpg")
