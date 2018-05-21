from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^amadon/add_to_cart$', views.add_to_cart),
    url(r'^amadon/checkout$', views.checkout),
    url(r'^go_back$', views.go_back),
    url(r'^clear$', views.clear)
]