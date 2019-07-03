import React, { Component } from "react";

class Pep extends Component {
  constructor(props) {
    super(props);
  }
  state = {
      
  };
  render() {
    return (
      <div>
        {this.state.username}
        <form onSubmit={this.handleSubmit}>
          <input
            onChange={this.handleChange}
            type="text"
            name="username"
            ref="username"
            id="username"
          />
          {this.state.password}

          <input
            onChange={this.handleChange}
            type="text"
            name="password"
            ref="password"
            id="password"
          />
          <input type="checkbox" name="pep" id="pep" ref="pep" />
          <button>save</button>
        </form>
      </div>
    );
  }
  onChange = () => {};
  handleSubmit = e => {
    e.preventDefault();
    const formData = {};

    for (const data in this.refs) {
      formData[data] = this.refs[data].value;
    }
    fetch("/default/index", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(formData)
    })
      .then(c => c.json())
      .then(c => {});
  };
  handleChange = e => {
    this.setState({ [e.target.name]: e.target.value });
  };
  create = () => {};
}

export default Pep;
