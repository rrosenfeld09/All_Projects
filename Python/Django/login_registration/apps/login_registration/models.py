from __future__ import unicode_literals
from django.db import models
import re
import bcrypt

EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9copy.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')


class UserManager(models.Manager):
    #validator for registration form
    def registration_validator(self, postData):
        errors = {}
        email_registered_check = User.objects.filter(email = postData['email'])
        if len(postData['first_name']) < 1:
            errors['first_name'] = "whoops, you didn't enter a first name!"
        elif postData['first_name'].isalpha() != True:
            errors['first_name'] = "oh, you're first name cannot contain a number.."
        elif len(postData['last_name']) < 1:
            errors['last_name'] = "whoops, you didn't enter a last name!"
        elif postData['last_name'].isalpha() != True:
            errors['last_name'] = "oh, you're last name cannot contain a number.."
        elif not EMAIL_REGEX.match(postData['email']):
            errors['email'] = "that's not a valid email address.."
        elif len(email_registered_check) > 0:
            errors['email'] = "that email has already been registered, please sign in"
        elif len(postData['password']) < 1:
            errors['password'] = "you forgot to enter a password"
        elif len(postData['password']) < 7:
            errors['password'] = "your password must be at least 8 characters long"
        elif postData['password'] != postData['confirm_password']:
            errors['password'] = "oh no, your passwords dont match!"
        return errors

        #validator for login form
    def login_validator(self, postData):
        errors = {}
        email_check = User.objects.filter(email = postData['login_email'])
        if len(email_check) == 0:
            errors['login_email'] = "that email isn't registered yet, please register"
            print "=" *100
            print "EMAIL NOT YET REGISTERED"
            print "=" *100
            return errors
        real_password = User.objects.filter(email = postData['login_email'])[0].password
        password_check = bcrypt.checkpw(postData['login_password'].encode(), real_password.encode())
        if password_check == False:
            errors['password'] = "that's not the right password"
        else:
            print "=" *100
            print "LOGGED IN"
            print "=" *100
        return errors



class User(models.Model):
    first_name = models.CharField(max_length = 255)
    last_name = models.CharField(max_length = 255)
    email = models.CharField(max_length = 255)
    password = models.CharField(max_length = 255)

    def __repr__(self):
        return "< first name: {}, last name: {}, email: {}, password: {} >".format(self.first_name, self.last_name, self.email, self.password)

    objects = UserManager()