export const baseHost = '/Admin'

export const auth = {
  login: baseHost + '/auth/ApiLogin',
  logout: baseHost + '/auth/ApiLogout',
  userInfo: baseHost + '/auth/ApiUserInfo'
}

export const account = {
  query: baseHost + '/Account/Query',
  getById: baseHost + '/Account/GetById',
  create: baseHost + '/Account/Create',
  edit: baseHost + '/Account/Edit',
  delete: baseHost + '/Account/Delete'
}

export const article = {
  category: {
    list: baseHost + '/Content/CategoryList',
    edit: baseHost + '/Content/CategoryEdit',
    delete: baseHost + '/Content/CategoryDelete',
    add: baseHost + '/Content/CategoryAdd'
  },
  content: {
    list: baseHost + '/Content/ContentList',
    listByCategory: baseHost + '/Content/ContentLstByCategory',
    listByUser: baseHost + '/Content/ContentListByUser',
    listByHostKey: baseHost + '/Content/ContentListByHostKey'
  }
}

export const  tip = {
  list: baseHost + '/Tip/JsonList',
  edit: baseHost + '/Tip/JsonEdit',
  delete: baseHost + '/Tip/JsonDelete',
  add: baseHost + '/Tip/JsonAdd'
}
