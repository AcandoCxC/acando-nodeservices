// uncomment this block and it should work
// const React = require('react')
// module.exports.default = () => React.createElement('div', null, 'Hello from server rendered React')

// however, we need this to work
import React from 'react' // make this work

// if .babelrc works (see config in package.json) the JSX should also work
export default () => (
  <div className='react'>
    Hello from server rendered React with ES6 stuff
  </div>
)