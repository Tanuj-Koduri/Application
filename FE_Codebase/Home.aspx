<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
  <head>
    <meta charset="utf-8"> <!-- Added charset declaration -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta tag for responsive design -->
    <meta content="origin" name="referrer">
    <title>Forbidden &middot; GitHub</title>
    <!-- Moved styles to an external CSS file for better separation of concerns -->
    <link rel="stylesheet" href="/styles/main.css">
    <!-- Added Content Security Policy header -->
    <meta http-equiv="Content-Security-Policy" content="default-src 'self'; img-src https:; object-src 'none';">
  </head>
  <body>
    <div class="container">
      <h1>Access to this site has been restricted.</h1>
      <p>
        <br>
        If you believe this is an error,
        please contact <a href="https://support.github.com" rel="noopener noreferrer">Support</a>. <!-- Added rel attribute for security -->
      </p>
      <div id="status-links">
        <a href="https://githubstatus.com" rel="noopener noreferrer">GitHub Status</a> &mdash;
        <a href="https://twitter.com/githubstatus" rel="noopener noreferrer">@githubstatus</a>
      </div>
    </div>
    <!-- Added defer attribute to improve page load performance -->
    <script src="/scripts/main.js" defer></script>
  </body>
</html>