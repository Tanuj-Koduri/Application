<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
  <head>
    <meta charset="UTF-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added viewport meta tag for responsiveness -->
    <meta content="origin" name="referrer">
    <title>Forbidden &middot; GitHub</title>
    <style>
      /* Moved styles to a separate stylesheet for better organization */
      /* Added some modern CSS features like CSS variables and flexbox */
      :root {
        --background-color: #f1f1f1;
        --text-color: #666;
        --link-color: #4183c4;
      }
      body {
        background-color: var(--background-color);
        margin: 0;
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Helvetica, Arial, sans-serif; /* Updated font stack */
        display: flex;
        justify-content: center;
        align-items: center;
        min-height: 100vh;
      }
      .container { 
        width: 100%;
        max-width: 800px; 
        text-align: center; 
        padding: 20px;
      }
      a { 
        color: var(--link-color); 
        text-decoration: none; 
        font-weight: bold; 
      }
      a:hover { text-decoration: underline; }
      h1 { color: var(--text-color); }
      #s {
        margin-top: 20px;
      }
      /* Removed unused styles */
    </style>
  </head>
  <body>
    <div class="container">
      <h1>Access to this site has been restricted.</h1>
      <p>
        If you believe this is an error,
        please contact <a href="https://support.github.com">Support</a>.
      </p>
      <div id="s">
        <a href="https://githubstatus.com">GitHub Status</a> &mdash;
        <a href="https://twitter.com/githubstatus">@githubstatus</a>
      </div>
    </div>
  </body>
</html>