from __future__ import unicode_literals
from django.db import models
from django.contrib import messages

class UserManager(models.Manager):
    def login_validator(self, request):
        errors = {}
        email_check = User.objects.filter(email = request.POST['login_email'])
        if len(email_check) > 0:
            real_password = User.objects.get(email= request.POST['login_email']).password
        if len(request.POST['login_email']) < 1:
            errors['email'] = "you didn't enter an email address!"
        if len(request.POST['login_password']) < 1:
            errors['password'] = "you didn't enter a password"
        if len(email_check) < 1:
            errors['login'] = "that email isn't registered yet, please register"
        elif real_password != request.POST['login_password']:
            errors['login_password'] = "that's not the correct password"
        return errors
    def registration_validator(self, request):
        errors = {}
        email_check = User.objects.filter(email = request.POST['email'])
        if len(request.POST['email']) < 1:
            errors['email'] = "you didn't enter an email address!"
        if len(request.POST['first_name']) < 1:
            errors['first_name'] = "you didn't enter your first name!"
        if len(request.POST['last_name']) < 1:
            errors['last_name'] = "you didn't enter your last name!"
        if len(request.POST['password']) < 8:
            errors['password'] = "your password must be at least 8 characters long!"
        if request.POST['password'] != request.POST['confirm_password']:
            errors['confirm_password'] = "your passwords don't match!"
        if len(email_check) > 0:
            errors['email_check'] = "that email is already registered, please sign in"
        return errors


class User(models.Model):
    first_name = models.CharField(max_length = 255)
    last_name = models.CharField(max_length = 255)
    email = models.CharField(max_length = 255)
    password = models.CharField(max_length = 255)
    desc = models.TextField()
    admin = models.BooleanField()
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    objects = UserManager()


class Message(models.Model):
    message = models.TextField()
    user = models.ForeignKey(User, related_name = "messages")
    written_by = models.CharField(max_length = 255)
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    objects = models.Manager()


class Comment(models.Model):    
    comment = models.TextField()
    user = models.ForeignKey(User, related_name = "comments")
    message = models.ForeignKey(Message, related_name = "comments")
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
