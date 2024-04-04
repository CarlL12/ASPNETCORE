
document.addEventListener('DOMContentLoaded', function () {
    let sw = document.getElementById('switch-mode');

      sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

          fetch(`/settings/changetheme?theme=${theme}`)
              .then(res => {
                  if (res.ok)
                      window.location.reload();
                  else
                      console.log("hello world!");
              })
    });

});