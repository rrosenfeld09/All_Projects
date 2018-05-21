from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^add_course$', views.add_course),
    url(r'^(?P<number>\d+)$', views.delete),
    url(r'^destroy/(?P<number>\d+)$', views.destroy)
]