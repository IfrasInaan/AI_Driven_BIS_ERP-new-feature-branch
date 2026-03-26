import os

def say_hello():
	return "Hello world! I'm a python script!"

def test(message):
	directory = os.getcwd()
	return message + " : " + directory