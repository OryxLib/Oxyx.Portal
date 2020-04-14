import request from '@/utils/request'
import '@/utils/serverHost'

export function loginByUsername(username, password) {
  const data = {
    username,
    password
  }
  return request({
    url: auth.login,
    method: 'post',
    data
  })
}

export function logout() {
  return request({
    url: auth.logout,
    method: 'post'
  })
}

export function getUserInfo() {
  return request({
    url: auth.userInfo,
    method: 'get'
  })
}

