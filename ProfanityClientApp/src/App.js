import React, { Component } from "react";
import axios from "axios";
import { Progress } from "reactstrap";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import "./App.css";
class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedFile: null,
      loaded: 0,
    };
  }

  // for checking file type, only text file is allowed by the api endpoint at this tiem
  checkFileType = (event) => {
    //getting file object
    let files = event.target.files;
    let err = [];
    // list allow file type (MIME), only text is supported at this moment
    const supportedFiletype = "text/plain";

    // loop through files array
    for (var x = 0; x < files.length; x++) {
      // compare file type find doesn't matach
      if (files[x].type !== supportedFiletype) {
        // create error message and assign to container
        err[x] =
          files[x].type + " is not a supported format for Profanity checker\n";
      }
    }
    for (var z = 0; z < err.length; z++) {
      // if message not same old that mean has error
      // discard selected file
      toast.error(err[z]);
      event.target.value = null;
    }
    return true;
  };
  // for checking minimum and maximum file size, maxFileSize = 12582912 bytes or 12 MB
  checkFileSize = (event) => {
    let files = event.target.files;
    let maxFileSize = 12582912; //size in bytes
    let err = [];
    for (var x = 0; x < files.length; x++) {
      console.log("Size of the uploaded file: " + files[x].size + "bytes");
      // for checking uploaded file size with maxFileSize limit
      if (files[x].size > maxFileSize) {
        err[x] = files[x].type + "is too large, please pick a smaller file\n";
      }
      // for checking uploaded file size with minimum file limit (0 Bytes)
      else if (files[x].size <= 0) {
        err[x] = files[x].type + "is too small, please pick a larger file\n";
      }
    }
    // if message not same old that mean has error
    for (var z = 0; z < err.length; z++) {
      // discard selected file
      toast.error(err[z]);
      event.target.value = null;
    }
    return true;
  };

  onChangeHandler = (event) => {
    var files = event.target.files;
    if (this.checkFileType(event) & this.checkFileSize(event)) {
      // if return true allow to setState
      this.setState({
        selectedFile: files,
        loaded: 0,
      });
    }
  };

  // for uploading file to API endpoint, after successul validation of checkFileType() and checkFileSize()
  onClickHandler = () => {
    const data = new FormData();
    const apiEndpoint = 'http://profanityapp.azurewebsites.net/api/profanity';
    for (var x = 0; x < this.state.selectedFile.length; x++) {
      data.append("file", this.state.selectedFile[x]);
    }
    axios.post(apiEndpoint, data, {
        onUploadProgress: (ProgressEvent) => {
          this.setState({
            loaded: (ProgressEvent.loaded / ProgressEvent.total) * 100,
          });
        },
      })
      .then((res) => {
        // then print response status if the status is OK (200) or print the error (500)
        if(res.statusText === "OK"){
          toast.success("Upload success. " + res.data);
        }
        else{
          toast.error("Something went wrong at server, Error: " + res.data);
        }
      })
      .catch((err) => {
        // then print response status
        toast.error("Upload Failed");
        console.log(err.data);
      });
  };

  render() {
    return (
      <div class="container">
        <div class="row">
          <div class="col-md-6 offset-md-3 col-sm-offset-1">
          <ToastContainer position="top-center"/>
            <div class="form-group files color">
              <label class="font-weight-bold">Upload file to check profanity</label>
              <input type="file" name="file" class="form-control" multiple="" onChange={this.onChangeHandler}/>
            </div>
            <div class="form-group">
              <Progress max="100" color="success" value={this.state.loaded}>
                {Math.round(this.state.loaded, 2)}%
              </Progress>
            </div>
            <button
              type="button"
              class="btn btn-success btn-block"
              onClick={this.onClickHandler}
            >
              Upload
            </button>
          </div>
        </div>
      </div>
    );
  }
}
export default App;
