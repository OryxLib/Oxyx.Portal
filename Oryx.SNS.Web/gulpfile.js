var gulp = require('gulp')

var spawn = require('child_process').spawn;

gulp.task('buildVue', function (done) {
    var cmd =(/^win/.test(process.platform) ? 'npm.cmd' : 'npm');
    spawn(cmd,['run','build:prod'],{cwd: './App/', stdio: 'inherit' }) .on('close', done) ; 
}); 
gulp.task('go', function () {
    gulp.watch('./App/src/**', gulp.series("buildVue"));
})