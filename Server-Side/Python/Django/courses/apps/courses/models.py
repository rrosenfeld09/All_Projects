from __future__ import unicode_literals

from django.db import models



class CourseManager(models.Manager):
    def basic_validator(self, postData):
        errors = {}
        if len(postData['name']) < 5:
            errors['name'] = "The course name should be more than 5 characters long"
        if len(postData['desc']) < 15: 
            errors['desc'] = "The description should be more than 15 characters long"
        if len(postData['desc']) > 300:
            errors['desc'] = "The descroption should not be more than 300 charcters long"
        return errors

class Course(models.Model):
    name = models.CharField(max_length=255)
    desc = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    
    def __repr__(self):
        return "< name: {}, description: {}, created_at{} >".format(self.name, self.desc, self.created_at)

    objects = CourseManager()