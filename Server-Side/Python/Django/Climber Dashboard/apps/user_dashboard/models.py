from __future__ import unicode_literals
from django.db import models
from django.contrib import messages
import bcrypt
import re
EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9copy.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')


class UserManager(models.Manager):
    def login_validator(self, request):
        errors = {}
        try:
            email_check = User.objects.filter(email = request.POST['login_email'])
            entered_password = User.objects.get(email = request.POST['login_email']).password
            password_check = bcrypt.checkpw(request.POST['login_password'].encode(), entered_password.encode())
            if len(request.POST['login_email']) < 1:
                errors['email'] = "you didn't enter an email address!"
            if len(request.POST['login_password']) < 1:
                errors['password'] = "you didn't enter a password"
            if len(email_check) < 1:
                errors['login'] = "that email isn't registered yet, please register"
            elif password_check == False:
                errors['login_password'] = "that's not the correct password"
            return errors
        except:
            errors['email'] = "please check the information entered"
            if User.objects.filter(email = request.POST['login_email']):
                return errors
            else:
                if len(request.POST['login_email']) < 1:
                    errors['email_len'] = "you didn't enter an email address..."
                else:
                    errors['not_registered'] = "doesn't look like that email is registered, please register"
            return errors
    def registration_validator(self, request):
        errors = {}
        email_check = User.objects.filter(email = request.POST['email'])
        if len(request.POST['email']) < 1:
            errors['email'] = "you didn't enter an email address!"
        if not EMAIL_REGEX.match(request.POST['email']):
            errors['email_valid'] = "that's not a valid email address"
        if len(request.POST['first_name']) < 1:
            errors['first_name'] = "you didn't enter your first name!"
        if len(request.POST['last_name']) < 1:
            errors['last_name'] = "you didn't enter your last name!"
        if len(request.POST['password']) < 8:
            errors['password'] = "your password must be at least 8 characters long!"
        if request.POST['password'] != request.POST['confirm_password']:
            errors['confirm_password'] = "your passwords don't match!"
        if len(email_check) > 0:
            errors['email_check'] = "that email is already registered"
        return errors
    def info_update_validator(self, request):
        errors = {}
        if not EMAIL_REGEX.match(request.POST['email']):
            errors['email_valid'] = "that's not a valid email address"
        return errors

    def pw_update_validator(self, request):
        errors = {}
        if len(request.POST['password']) < 7:
            errors['password_len'] = "passwords must be at least 8 characters long"
        if request.POST['password'] != request.POST['confirm_pw']:
            errors['password'] = "those passwords don't match!"
        return errors

    def desc_update_validator(self, request):
        errors = {}
        if len(request.POST['desc']) > 500:
            errors['desc'] = "that description is too long"
        return errors


class MessageManager(models.Manager):
    def message_validator(self, request):
        errors = {}
        if len(request.POST['textarea']) < 1:
            errors['message'] = "you didn't enter a message"
        return errors

class CommentManager(models.Manager):
    def comment_validator(self, request):
        errors = {}
        if len(request.POST['comment']) < 1:
            errors['comment'] = "you didn't enter a comment"
        return errors


class User(models.Model):
    first_name = models.CharField(max_length = 255)
    last_name = models.CharField(max_length = 255)
    email = models.CharField(max_length = 255)
    password = models.CharField(max_length = 255)
    desc = models.TextField()
    admin = models.BooleanField()
    created_at = models.DateField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    objects = UserManager()


class Message(models.Model):
    message = models.TextField()
    user = models.ForeignKey(User, related_name = "messages")
    written_by = models.ForeignKey(User, related_name="who_wrote_messages")
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    objects = MessageManager()


class Comment(models.Model):    
    comment = models.TextField()
    user = models.ForeignKey(User, related_name = "comments")
    message = models.ForeignKey(Message, related_name = "comments")
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    objects = CommentManager()
