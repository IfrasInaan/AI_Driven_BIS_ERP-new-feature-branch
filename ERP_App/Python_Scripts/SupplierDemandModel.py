import pickle
import numpy as np
from keras.models import load_model
from keras.losses import MeanSquaredError
import pandas as pd
import os

def get_model_path(filename):
    script_dir = os.path.dirname(os.path.abspath(__file__))
    trained_model_dir = os.path.join(script_dir, "TrainedModelFile")
    return os.path.join(trained_model_dir, filename)

try:
    with open(get_model_path("Purchase_Demand_Cost_model.pkl"), "rb") as f:
        data_processor = pickle.load(f)

    lstm_model = load_model(get_model_path("Supplier_Demand_TotalAmount_model.h5"), custom_objects={'mse': MeanSquaredError()})
except Exception as e:
    raise RuntimeError(f"Error loading models: {str(e)}")

def predict_supplier_total_amount(month, supplier_name):
    try:
        month_num = pd.to_datetime(month).month
        features = np.array([[month_num, supplier_name]])
        features_transformed = data_processor.transform(features)
        prediction = lstm_model.predict(features_transformed)
        return max(0, float(prediction[0][0]))
    except Exception as e:
        raise RuntimeError(f"Prediction error: {str(e)}")
