import purgeCSS from "gulp-purgecss";
import concat from "gulp-concat";
import cleanCSS from "gulp-clean-css";
import { src, dest } from "gulp";

const pathToCss = 'SportTracker.Server/wwwroot/css';

function bundleCss() {
    return src([
        // needs to be explicit to keep the order when concatenating
        `${pathToCss}/blazor-ui.css`,
        `${pathToCss}/halfmoon.css`,
        `${pathToCss}/halfmoon.modern.css`,
    ])
        .pipe(concat(`styles.min.css`))
        .pipe(purgeCSS({
            content: ["SportTracker.Server/Components/**/*.razor"],
            variables: true,
        }))
        .pipe(cleanCSS())
        .pipe(dest(pathToCss))
}

export default bundleCss;

