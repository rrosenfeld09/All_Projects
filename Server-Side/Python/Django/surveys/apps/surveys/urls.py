from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^surveys/process$', views.process),
    url(r'^results$', views.results),
    url(r'^go_back$', views.go_back),
    url(r'^reset_session$', views.reset)
]