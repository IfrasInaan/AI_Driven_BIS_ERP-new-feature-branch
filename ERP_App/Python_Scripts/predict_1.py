import joblib

# Load the model
model = joblib.load("iris_model.pkl")

# Define the predict function
def predict(features):
    return model.predict([features])[0]