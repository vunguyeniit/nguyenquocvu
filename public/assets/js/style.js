let x = document.querySelectorAll(".fa-regular");
let y = document.querySelectorAll(".showpass");

for (let j = 0; j < x.length; j++) {

  x[j].onclick = function () {

    for (let i = 0; i < y.length; i++) {

      if (y[i].type == "password") {

        y[i].type = "text";
      } else {
        y[i].type = "password";
      }
    }
  }
}


// SIDEBAR DROPDOWN
const allDropdown = document.querySelectorAll('#sidebar .side-dropdown');
const sidebar = document.getElementById('sidebar');

allDropdown.forEach(item => {
  const a = item.parentElement.querySelector('a:first-child');
  a.addEventListener('click', function (e) {
    e.preventDefault();

    if (!this.classList.contains('active')) {
      allDropdown.forEach(i => {
        const aLink = i.parentElement.querySelector('a:first-child');

        aLink.classList.remove('active');
        i.classList.remove('show');
      })
    }

    this.classList.toggle('active');
    item.classList.toggle('show');
  })
})

let caret = document.querySelectorAll('.box-icon');
let dropd = document.querySelectorAll('.dropdown-content');
console.log(caret);



// console.log(j);

x.onclick = function () {

  for (let i = 0; i < dropd.length; i++) {

    if (dropd[i].style.display === 'none') {
      dropd[i].style.display = 'block';
    }
    else {
      dropd[i].style.display = 'none';
    }
  }
}



// SIDEBAR COLLAPSE
// const toggleSidebar = document.querySelector('nav .toggle-sidebar');
// const allSideDivider = document.querySelectorAll('#sidebar .divider');

// if (sidebar.classList.contains('hide')) {
//   allSideDivider.forEach(item => {
//     item.textContent = '-'
//   })
//   allDropdown.forEach(item => {
//     const a = item.parentElement.querySelector('a:first-child');
//     a.classList.remove('active');
//     item.classList.remove('show');
//   })
// } else {
//   allSideDivider.forEach(item => {
//     item.textContent = item.dataset.text;
//   })
// }

// toggleSidebar.addEventListener('click', function () {
//   sidebar.classList.toggle('hide');

//   if (sidebar.classList.contains('hide')) {
//     allSideDivider.forEach(item => {
//       item.textContent = '-'
//     })

//     allDropdown.forEach(item => {
//       const a = item.parentElement.querySelector('a:first-child');
//       a.classList.remove('active');
//       item.classList.remove('show');
//     })
//   } else {
//     allSideDivider.forEach(item => {
//       item.textContent = item.dataset.text;
//     })
//   }
// })




// sidebar.addEventListener('mouseleave', function () {
//   if (this.classList.contains('hide')) {
//     allDropdown.forEach(item => {
//       const a = item.parentElement.querySelector('a:first-child');
//       a.classList.remove('active');
//       item.classList.remove('show');
//     })
//     allSideDivider.forEach(item => {
//       item.textContent = '-'
//     })
//   }
// })



// sidebar.addEventListener('mouseenter', function () {
//   if (this.classList.contains('hide')) {
//     allDropdown.forEach(item => {
//       const a = item.parentElement.querySelector('a:first-child');
//       a.classList.remove('active');
//       item.classList.remove('show');
//     })
//     allSideDivider.forEach(item => {
//       item.textContent = item.dataset.text;
//     })
//   }
// })




// // PROFILE DROPDOWN
// const profile = document.querySelector('nav .profile');
// const imgProfile = profile.querySelector('img');
// const dropdownProfile = profile.querySelector('.profile-link');

// imgProfile.addEventListener('click', function () {
//   dropdownProfile.classList.toggle('show');
// })




// // MENU
// const allMenu = document.querySelectorAll('main .content-data .head .menu');

// allMenu.forEach(item => {
//   const icon = item.querySelector('.icon');
//   const menuLink = item.querySelector('.menu-link');

//   icon.addEventListener('click', function () {
//     menuLink.classList.toggle('show');
//   })
// })



// window.addEventListener('click', function (e) {
//   if (e.target !== imgProfile) {
//     if (e.target !== dropdownProfile) {
//       if (dropdownProfile.classList.contains('show')) {
//         dropdownProfile.classList.remove('show');
//       }
//     }
//   }

//   allMenu.forEach(item => {
//     const icon = item.querySelector('.icon');
//     const menuLink = item.querySelector('.menu-link');

//     if (e.target !== icon) {
//       if (e.target !== menuLink) {
//         if (menuLink.classList.contains('show')) {
//           menuLink.classList.remove('show')
//         }
//       }
//     }
//   })
// })





// // PROGRESSBAR
// const allProgress = document.querySelectorAll('main .card .progress');

// allProgress.forEach(item => {
//   item.style.setProperty('--value', item.dataset.value)
// })






// // APEXCHART
// var options = {
//   series: [{
//     name: 'series1',
//     data: [31, 40, 28, 51, 42, 109, 100]
//   }, {
//     name: 'series2',
//     data: [11, 32, 45, 32, 34, 52, 41]
//   }],
//   chart: {
//     height: 350,
//     type: 'area'
//   },
//   dataLabels: {
//     enabled: false
//   },
//   stroke: {
//     curve: 'smooth'
//   },
//   xaxis: {
//     type: 'datetime',
//     categories: ["2018-09-19T00:00:00.000Z", "2018-09-19T01:30:00.000Z", "2018-09-19T02:30:00.000Z", "2018-09-19T03:30:00.000Z", "2018-09-19T04:30:00.000Z", "2018-09-19T05:30:00.000Z", "2018-09-19T06:30:00.000Z"]
//   },
//   tooltip: {
//     x: {
//       format: 'dd/MM/yy HH:mm'
//     },
//   },
// };

// var chart = new ApexCharts(document.querySelector("#chart"), options);
// chart.render();