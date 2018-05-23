from __future__ import unicode_literals
from django.db import models
from django.contrib import messages
from django.db import models
from django.shortcuts import render, redirect


class UserManager(models.Manager):
    def registration_validator(self, request):
        valid = True
        if len(request.POST['name']) < 1:
            messages.error(request, "please enter a name")
            valid = False
        if len(request.POST['email']) < 1:
            messages.error(request, "please enter an email")
            valid = False 
        if valid == True:
            User.objects.create(name = request.POST['name'], email = request.POST['email'])
            print "REGISTERED"
            return redirect('/success')
        else: 
            print "DID NOT REGISTER"
            return redirect('/')




class User(models.Model):
    name = models.CharField(max_length = 255)
    email = models.CharField(max_length = 255)
    # creted_at
    # updated_at
    objects = UserManager()


