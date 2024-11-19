<!DOCTYPE html>
```

2. Add language attribute to the html tag:
```html
<html lang="en">
```

3. Use more semantic HTML5 elements:
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
<footer id="s">
  <a href="https://githubstatus.com">GitHub Status</a> &mdash;
  <a href="https://twitter.com/githubstatus">@githubstatus</a>
</footer>
```

4. Consider using external CSS file instead of inline styles for better maintainability.

5. Update the media query for high-resolution displays:
```css
@media
only screen and (min-resolution: 192dpi),
only screen and (min-resolution: 2dppx) {
  .logo-img-1x { display: none; }
  .logo-img-2x { display: inline-block; }
}
```

6. Add meta viewport tag for better responsive design:
```html
<meta name="viewport" content="width=device-width, initial-scale=1.0">
```

7. Consider adding some basic accessibility attributes:
```html
<a href="https://support.github.com" aria-label="Contact GitHub Support">Support</a>