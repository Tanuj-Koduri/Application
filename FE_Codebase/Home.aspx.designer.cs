<!DOCTYPE html>
```

2. Add a language attribute to the html tag:
```html
<html lang="en">
```

3. Add a viewport meta tag for better responsive design:
```html
<meta name="viewport" content="width=device-width, initial-scale=1.0">
```

4. Use more semantic HTML5 elements:
```html
<header>
  <h1>Access to this site has been restricted.</h1>
</header>
<main>
  <p>
    <br>
    If you believe this is an error,
    please contact <a href="https://support.github.com">Support</a>.
  </p>
</main>
<footer>
  <div id="s">
    <a href="https://githubstatus.com">GitHub Status</a> &mdash;
    <a href="https://twitter.com/githubstatus">@githubstatus</a>
  </div>
</footer>
```

5. Consider moving the CSS to a separate file for better maintainability.

6. Update the media query to use more modern syntax:
```css
@media (min-resolution: 192dpi), (min-resolution: 2dppx) {
  .logo-img-1x { display: none; }
  .logo-img-2x { display: inline-block; }
}