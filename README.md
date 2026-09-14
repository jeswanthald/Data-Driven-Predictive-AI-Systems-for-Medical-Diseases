# Data-Driven-Predictive-AI-Systems-for-Medical-Diseases
This project introduces an AI-based medical assistant built with Windows Forms, designed to predict four common diseases such as Heart Disease, Stroke, Diabetes, Vitamin Deficiency.

An AI-based medical prediction system that combines Machine Learning,
Deep Learning, Flask, REST APIs, and C# Windows Forms to support
early prediction of common diseases.

## 🚀 Project Overview

The system predicts:

- Heart Disease
- Stroke
- Diabetes
- Vitamin Deficiency

Machine Learning algorithms such as Decision Tree and Random Forest
are used for structured patient data, while a CNN model is used for
image-based vitamin deficiency detection.

## 🎯 Objectives

- Predict diseases using patient health data.
- Detect vitamin deficiency using medical images.
- Combine Machine Learning and Deep Learning.
- Provide real-time prediction results.
- Provide a user-friendly Flask web interface.
- Provide a C# Windows Forms interface for doctors.
- Enable communication between patients and doctors through chat.

## 🛠️ Technologies Used

### Programming Languages
- Python
- C#

### Machine Learning
- Pandas
- scikit-learn
- Decision Tree
- Random Forest

### Deep Learning
- TensorFlow
- Keras
- CNN

### Backend
- Flask
- REST API

### Desktop Application
- C#
- .NET
- Windows Forms

## 🏗️ System Architecture

The project follows a client-server architecture.

```text
          Patient/User
               |
               v
        Flask Web Application
               |
               v
            REST API
               |
               v
          Python Backend
               |
     +-------------------+
     |                   |
     v                   v
Machine Learning       CNN Model
Decision Tree          Image Analysis
Random Forest              |
     |                      |
     +----------+-----------+
                |
                v
        Prediction Result
                |
                v
       C# Windows Forms
         Doctor Application
                |
                v
          Doctor Feedback
```
## 📸 Screenshots

### 🔄 Workflow

![Workflow](./Screenshots/WorkFlow.png)

### 📊 Sequence Diagram

![Sequence Diagram](./Screenshots/Sequence%20Diagram.png)

### 🩺 Disease Deficiency Detection

![Disease Deficiency Detection](./Screenshots/Disease%20Deficiency%20Detection.png)

### 😊 Emoji Feedback

![Emoji Feedback](./Screenshots/Emoji%20Feedback.png)

### 🧬 Predict Vitamin Page

![Predict Vitamin](./Screenshots/Predict%20Vitamin%20page.png)

### 🔬 Vitamin Detection Result

![Vitamin Detection Result](./Screenshots/Result%20Vitamin.png)

### 📥 Inbox Interface – Desktop Application

![Inbox Interface](./Screenshots/Inbox%20Interface%20%28Desktop%20Application%29.png)


### 💻 Reply Interface – Desktop Application

![Reply Interface](./Screenshots/Reply%20Interface%20%28Desktop%20Application%29.png)

### 💬 Web Chat Interface

![Web Chat Interface](./Screenshots/Web%20Chat%20Interface.png)

## 💻 System Requirements

### Hardware Requirements

| Component | Requirement |
|---|---|
| System | Intel Core i3 |
| RAM | 4 GB |
| Hard Disk | 100 GB |
| Processor | 3.30 GHz |

### Software Requirements

| Software | Requirement |
|---|---|
| Operating System | Windows 11 |
| Programming Languages | Python, C# |
| Development Tool | Anaconda |
| Desktop Framework | Windows Forms |

## ▶️ How to Run

### 1. Download the Project

Download or clone this repository to your local computer.

### 2. Set Up the Python Environment

Open Anaconda Prompt and navigate to the project folder.

### 3. Install Required Packages

Install the required Python libraries.
```bash
pip install -r requirements.txt
```

### 4. Run the Flask Web Application

Start the Flask application.
```bash
python app.py
```

### 5. Run the Doctor Application

Open the C# Windows Forms project in Visual Studio.
Build the project and run the Doctor Application.

### 6. Use the Application

#### Patient/User

1. Login or register.
2. Enter the required health information.
3. Submit the information for disease prediction.
4. Upload a medical image for vitamin deficiency detection.
5. View the prediction result.
6. Communicate with the doctor through the Web Chat Interface.

#### Doctor

1. Login to the Doctor Application.
2. View patient submissions in the Inbox Interface.
3. Review prediction results.
4. Review uploaded medical images.
5. Communicate with patients.
6. Provide feedback through the Reply Interface.

