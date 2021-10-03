import React, { Component } from "react";
import MemberListComponent from "./MemberListComponent";
export class Members extends Component {
  static displayName = "Permit Management";

  constructor(props) {
    super(props);
    this.state = { members: [], loading: true };
    this.handleClick = this.handleClick.bind(this);
    this.renderMembersTable = this.renderMembersTable.bind(this);
  }

  componentDidMount() {
    this.populateMembersData();
  }

  handleClick() {
    fetch("http://localhost:56644/permit/GetDisributePermits", {
      method: "POST",
    }).then((response) => {
      if (response.ok) {
        this.populateMembersData();
      } else {
        alert("Beklenmeyen bir hata oluştu.");
        console.log(response);
      }
    });
  }

  renderMembersTable(members) {
    //debugger;
    return (
      <div>
        <div className="row">
          <button className="btn btn-primary right" onClick={this.handleClick}>
            Distribute
          </button>
        </div>
        <div className="row mt-3">
          <MemberListComponent members={members} />
        </div>
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderMembersTable(this.state.members)
    );

    return (
      <div>
        <h1 id="tabelLabel">Members</h1>
        <p>This component created by Admin.</p>
        {contents}
      </div>
    );
  }

  async populateMembersData() {
    const response = await fetch("http://localhost:56644/permit/GetPermits");
    const data = await response.json();
    this.setState({ members: data, loading: false });
  }
}
