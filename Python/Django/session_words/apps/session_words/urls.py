from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^session_words$', views.index),
    url(r'^session_words/add_word$', views.add_word),
    url(r'^session_words/clear$', views.clear)
]