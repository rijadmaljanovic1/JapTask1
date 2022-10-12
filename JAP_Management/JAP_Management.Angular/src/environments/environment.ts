// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

//localhost
let _webAPIBaseURL = 'https://localhost:7289';

//docker
//let _webAPIBaseURL = 'http://localhost:5001';

let studentURL = `${_webAPIBaseURL}/api/Student`;
let programURL = `${_webAPIBaseURL}/api/Program`;
let selectionURL = `${_webAPIBaseURL}/api/Selection`;
let authURL = `${_webAPIBaseURL}/api/Auth`;
let ranksURL = `${_webAPIBaseURL}/api/Rank`;

export const environment = {
  production: false,

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

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
