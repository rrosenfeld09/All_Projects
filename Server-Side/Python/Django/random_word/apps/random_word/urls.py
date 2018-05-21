from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^clear/session$', views.clear_session),
    url(r'^generate$', views.generate)
]