var url = url = new URL(window.location.href)
path = url.pathname
menu_name = path.split('/')[1] || 'Home'
itemMenuActive = '#link-' + menu_name
$(function () {
  $(itemMenuActive).addClass('active')
})