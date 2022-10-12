//localhost
let _webAPIBaseURL = 'https://localhost:7289';

//docker
//let _webAPIBaseURL = 'http://localhost:5001';

let studentURL = `${_webAPIBaseURL}/api/Student/`;
let programURL = `${_webAPIBaseURL}/api/Program`;
let selectionURL = `${_webAPIBaseURL}/api/Selection`;
let authURL = `${_webAPIBaseURL}/api/Auth`;
let ranksURL = `${_webAPIBaseURL}/api/Rank`;

export const environment = {
  production: true,

  webAPIBaseURL: _webAPIBaseURL,

  // student URLs
  studentsURL: `${studentURL}`,
  commentStudentByIdURL: `${studentURL}/comment`,

  // program URLs
  programsURL: `${programURL}`,

  // selection URLs
  selectionsURL: `${selectionURL}`,

  //rank
  rankURL: `${ranksURL}`,

  // AUTH URLs
  loginURL: `${authURL}/login`,

  noPhotoPath: 'assets/no-photo.png',
  loaderGifPath:'assets/loading.gif',
  noResultsPhoto:'assets/no-results.png'

};