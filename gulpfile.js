var gulp = require('gulp'),
    webserver = require('gulp-webserver');

gulp.task('webserver', function () {
    gulp.src('./Repository/Ex3-Bootstrap/')
        .pipe(webserver({
            livereload: true,
            open: true,
            fallback: 'products.html'
        }));
});