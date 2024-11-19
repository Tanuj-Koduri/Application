<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head>
    <meta charset="UTF-8"> <!-- Added charset declaration -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added viewport meta for responsiveness -->
    <meta content="origin" name="referrer">
    <title>Forbidden &middot; GitHub</title>
    <style>
        /* Moved styles to a separate stylesheet for better organization */
        @import url('styles.css');
    </style>
    <!-- Added Content Security Policy header -->
    <meta http-equiv="Content-Security-Policy" content="default-src 'self'; img-src https:; object-src 'none';">
</head>
<body>
    <div class="container">
        <h1>Access to this site has been restricted.</h1>
        <p>
            <br>
            If you believe this is an error,
            please contact <a href="https://support.github.com" rel="noopener noreferrer">Support</a>.
        </p>
        <div id="status">
            <a href="https://githubstatus.com" rel="noopener noreferrer">GitHub Status</a> &mdash;
            <a href="https://twitter.com/githubstatus" rel="noopener noreferrer">@githubstatus</a>
        </div>
    </div>
    <!-- Added defer attribute to improve page load performance -->
    <script src="script.js" defer></script>
</body>
</html>