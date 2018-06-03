from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^login$', views.login),
    url(r'^login_process$', views.login_process),
    url(r'^register$', views.register),
    url(r'^register_process$', views.register_process),
    url(r'^dashboard$', views.dashboard),
    url(r'^dashboard/admin$', views.admin_dashboard),
    url(r'^users/new$', views.new_user),
    url(r'^admin_add_user$', views.admin_add),
    url(r'^users/show/(?P<number>\d+)$', views.show),
    url(r'^logout$', views.logout),
    url(r'^delete/(?P<number>\d+)$', views.delete),
    url(r'^users/edit/(?P<number>\d+)$', views.edit),
    url(r'^admin/edit/(?P<number>\d+)$', views.admin_edit),
    url(r'^admin/password_update/(?P<number>\d+)$', views.admin_password),
    url(r'^add_message/(?P<number>\d+)$', views.add_message),
    url(r'^comment/(?P<number>\d+)$', views.add_comment),
    url(r'^message/remove/(?P<number>\d+)$', views.remove_message),
    url(r'^comment/remove/(?P<number>\d+)$', views.remove_comment),
    url(r'^users/edit_self/(?P<number>\d+)$', views.user_edit),
    url(r'^self_edit/info/(?P<number>\d+)$',views.self_edit_info),
    url(r'^self_edit/pw/(?P<number>\d+)$',views.self_edit_pw),
    url(r'^self_edit/desc/(?P<number>\d+)$',views.self_edit_desc),
    url(r'^invitation_code$', views.invitation_code)

]