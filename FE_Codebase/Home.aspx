<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for better accessibility -->
  <head>
    <meta charset="utf-8"> <!-- Added charset declaration -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta tag for responsive design -->
    <meta content="origin" name="referrer">
    <title>Forbidden &middot; GitHub</title>
    <!-- Moved styles to an external CSS file for better separation of concerns -->
    <link rel="stylesheet" href="styles.css">
    <!-- Added Content Security Policy header -->
    <meta http-equiv="Content-Security-Policy" content="default-src 'self'; img-src https:; object-src 'none';">
  </head>
  <body>
    <div class="container">
      <h1>Access to this site has been restricted.</h1>
      <p>
        <br>
        If you believe this is an error,
        please contact <a href="https://support.github.com">Support</a>.
      </p>
      <div id="status-links">
        <a href="https://githubstatus.com">GitHub Status</a> &mdash;
        <a href="https://twitter.com/githubstatus">@githubstatus</a>
      </div>
    </div>
    <!-- Added defer attribute to load script asynchronously -->
    <script src="script.js" defer></script>
  </body>
</html>
```

Here are the main changes and improvements:

1. Added `lang` attribute to the `<html>` tag for better accessibility.
2. Added `charset` declaration and viewport meta tag for responsive design.
3. Moved styles to an external CSS file (`styles.css`) for better separation of concerns.
4. Added a Content Security Policy header to enhance security.
5. Removed inline styles and media queries (moved to external CSS).
6. Changed the `id` of the status links div to be more descriptive.
7. Added a deferred script tag at the end of the body for any potential JavaScript.

For the CSS, create a file named `styles.css` with the following content:

```css
body {
  background-color: #f1f1f1;
  margin: 0;
  font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Helvetica, Arial, sans-serif;
}

.container {
  margin: 30px auto 40px auto;
  max-width: 800px;
  text-align: center;
}

a {
  color: #4183c4;
  text-decoration: none;
  font-weight: bold;
}

a:hover {
  text-decoration: underline;
}

h1, h2, h3 {
  color: #666;
}

#status-links {
  margin-top: 25px;
}

#status-links a {
  display: inline-block;
  margin: 10px 25px;
}

@media (min-width: 768px) {
  .container {
    width: 800px;
  }
}