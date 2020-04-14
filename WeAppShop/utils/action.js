var threadHandler = []
var waitHandler = {}
var index = 0
function action(threadFunc) {
  var threadName = 'default';
  var tid = threadName + (new Date() - 0) + (index++)
  var threadObj = {
    id: tid,
    name: threadName,
    run: function (cb) {
      cb && cb(threadObj)
    },
    waitAll: function (cb) {
      threadHandler.filter(function (item) {
        return item.id == tid
      })[0].status = 1
      waitHandler[threadObj.name] = function () {
        cb && cb(threadObj)
      }
    },
    end: function () {
      threadHandler.filter(function (item) {
        return item.id == tid
      })[0].status = 1
      threadObj.checkWaitHandler()
    },
    checkWaitHandler: function () {
      var isAllEnd = threadHandler.every(function (item) {
        return item.status == 1
      })
      isAllEnd && waitHandler[threadObj.name]();
    }
  }
  threadHandler.push({
    name: threadName,
    id: tid,
    status: 0
  })
  threadFunc(threadObj)

}
module.exports = {
  action
}
