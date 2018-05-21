from django.shortcuts import render, redirect, HttpResponse

from django.contrib import messages

from models import *

def index(request):
    context = {
        'courses': Course.objects.order_by("-created_at")
    }
    return render(request, "index.html", context)

def add_course(request):
    errors = Course.objects.basic_validator(request.POST)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
    else:
        Course.objects.create(name = request.POST['name'], desc = request.POST['desc'])
    return redirect("/")

def destroy(request, number):
    context = {
        'course': Course.objects.get(id = number)
    }
    return render(request, "destroy.html", context)

def delete(request, number):
    Course.objects.get(id = number).delete()
    return redirect("/")