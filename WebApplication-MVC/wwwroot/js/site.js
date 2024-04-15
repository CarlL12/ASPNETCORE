
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

    select()
    searchQuery()


});


function searchQuery() {
    try {

        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            updateCourseByFilter()
        })
    }
    catch {

    }
}

function select() {

    try {
        let select = document.querySelector('.select')
        let selected = select.querySelector('.selected')
        let selectOptions = select.querySelector('.select-options')

        selected.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.textContent = this.textContent;
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value', category)
                updateCourseByFilter()
            })
        })
    }

    catch {

    }
 
}

function updateCourseByFilter() {

    const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
    const searchQuery = document.querySelector('#searchQuery').value
    const url = `/account/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.articles').innerHTML = dom.querySelector('.articles').innerHTML

            const pagination = dom.querySelector('.button') ? dom.querySelector('.button').innerHtml : ''
            document.querySelector('.button').innerHtml = pagination
        })
}
function saveCourse(courseId) {
    var returnUrl = window.location.pathname;
    fetch(`/Account/SaveCourse?id=${courseId}&returnUrl=${encodeURIComponent(returnUrl)}`)
        .then(response => response.text())
        .then(data => {

           window.location.reload();


        })
        .catch(error => console.error('Error:', error));
}