import pickle
import numpy as np
from keras.models import load_model
from keras.losses import MeanSquaredError
from sklearn.preprocessing import OneHotEncoder
from sklearn.compose import ColumnTransformer
from sklearn.pipeline import Pipeline
import pandas as pd
import os

def get_model_path(filename):
    """Helper function to get absolute path to model files"""
    script_dir = os.path.dirname(os.path.abspath(__file__))
    trained_model_dir = os.path.join(script_dir, "TrainedModelFile")
    return os.path.join(trained_model_dir, filename)

# Load models
try:
    with open(get_model_path("Product_Demand_model.pkl"), "rb") as f:
        data_processor = pickle.load(f)  # Handles encoding

    lstm_model = load_model(get_model_path("Product_Demand_model.h5"), custom_objects={'mse': MeanSquaredError()})
except Exception as e:
    raise RuntimeError(f"Error loading models: {str(e)}")

def predict_demand(month, product_name):
    """
    Predict demand based on month and product name.
    Example input:
        month = '2017-10'
        product_name = 'A K Fancy Bag'
    """
    try:
        month_num = pd.to_datetime(month).month

        # Encode product name
        product_encoded = encoder.transform([product_name])

        features = np.array([[month_num, product_encoded[0]]])
        features_transformed = data_processor.transform(features)

        prediction = lstm_model.predict(features_transformed)
        predicted_demand = max(0, float(prediction[0][0]))

        return predicted_demand
    except Exception as e:
        raise RuntimeError(f"Prediction error: {str(e)}")