var gulp = require('gulp');
var del = require('del');
var browserSync = require('browser-sync');
var uglify = require('gulp-uglify');
var usemin = require('gulp-usemin');
var minifyHtml = require('gulp-minify-html');
var rev = require('gulp-rev');


gulp.task('browserSync', function() {
    browserSync.init({
        server: {},
        files: ['**/*.*'],
    });
});