// to make ES6 stuff work we need babel-core/register
// when we enable that, the page crashes
// if we make it not crash, we can enable import statements and 
// JSX work straight from node and across the project 

// require('babel-core/register')
// you can also try uncommenting babel-core/register and use, its the same thing
// require('babel-register')
require('babel-polyfill')

const path = require('path')
const fs = require('fs')
const React = require('react')
const { renderToString } = require('react-dom/server')

const Client = require('./React/index').default

module.exports = function (callback, options) {

  const filePath = path.resolve(__dirname, 'public', 'index.html')

  fs.readFile(filePath, 'utf8', (err, htmlData) => {
    if (err) return callback(null, err)
    const markup = renderToString( React.createElement(Client) )
    const RenderedApp = htmlData.replace('{{SSR}}', markup)
    callback(null, RenderedApp)
  })

}

// To test the output of the above code, comment it out and uncomment this block
// then simply run `node src/react.server.js` to see the console.log return

// let debug = true
// if (debug === true) {
//   const filePath = path.resolve(__dirname, 'public', 'index.html') // works
//   fs.readFile(filePath, 'utf8', (err, htmlData) => {
//     if (err) {
//       console.log(err)
//     }
//     const markup = renderToString( React.createElement(Client) )
//     const RenderedApp = htmlData.replace('{{SSR}}', markup)
//     console.log(RenderedApp)
//   })
// }