from django.shortcuts import render, redirect, HttpResponse
from models import User
from django.contrib import messages

def index(request):
    return render(request, 'index.html')

def register(request):
    User.objects.registration_validator(request)
    return redirect('/')

def success(request):
    return render(request, 'success.html')
