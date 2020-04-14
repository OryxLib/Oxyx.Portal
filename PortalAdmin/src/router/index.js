import Vue from 'vue'
import Router from 'vue-router'


import Home from '@/components/Home'
// import HelloWorld from '@/components/HelloWorld'
// import Test from '@/components/Test'
// import Login from '@/components/Login'
// import Category from '@/components/Categories'
// import Commodity from '@/components/Commodities'
// import Order from '@/components/Orders'
// import Payment from '@/components/PaymentRecord'

// 编辑器
import VueQuillEditor from 'vue-quill-editor'
// import { container, ImageExtend, QuillWatch } from 'quill-image-extend-module'
// Quill.register('modules/ImageExtend', ImageExtend)
// require styles
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'


Vue.use(Router)
Vue.use(VueQuillEditor)
Vue.use(VueResource)
Vue.http.options.emulateJSON = true;
Vue.http.options.emulateHTTP = true;
Vue.http.options.root="http://localhost:21763" 

const requireComponent = require.context(
  // 组件目录的相对路径
  '@/components',
  // 是否查询其子目录
  true,
  // 匹配基础组件文件名的正则表达式
  /\w+\.(vue)$/i
)
let routes = [];
requireComponent.keys().forEach(fileName => {
  // 获取组件配置
  const componentConfig = requireComponent(fileName)
  // 获取组建的PascalCase命名
  // const componentName = upperFirst(
  //   camelCase(
  //     // 替换文件名开头的`./`和结尾的扩展名
  //     fileName.replace(/^\.\/(.*)\.\w+$/, '$1')
  //   )
  // )
  var path = fileName.replace(/^\.\/+(.*)\.\w+$/, '$1');
  var name = fileName.match(/(\w*)\.\w+/)[1]
  console.log(fileName.replace(/^\.\/+(.*)\.\w+$/, '$1'))
  var component = require('@/components/' + path).default;
  if (name == "Home") {
    var chome = require('@/components/Home');
    console.log(chome.default)
    console.log(component)
    console.log(Home)
    routes.push({
      path: '/',
      name: name,
      component: component// requireComponent(name, componentConfig)
    })
  }
  else {
    routes.push({
      path: '/' + path,
      name: name,
      component: component//requireComponent(name, componentConfig)
    })
  }
})
 
export default new Router({ routes: routes }) 