from django.shortcuts import render, redirect, HttpResponse

def register(request):
    return HttpResponse("placeholder for users to create a new user record")

def login(request):
    return HttpResponse("places holder for users to login")

def index(request):
    return HttpResponse("placeholder to later display a list of users")