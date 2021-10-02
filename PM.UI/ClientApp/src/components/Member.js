import React, { Component } from "react";

export class Member extends Component {
  static displayName = Member.name;

  constructor(props) {
    super(props);
    this.state = { members: [], loading: true };
  }

  componentDidMount() {
    this.populateMemberData();
  }

  static renderMembersTable(members) {
    debugger;
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>İsim</th>
            <th>Soyisim</th>
            <th>Ünvan</th>
            <th>İzin Başlanğıç Tarihi</th>
            <th>İzin Bitiş Tarihi</th>
          </tr>
        </thead>
        <tbody>
          {members.map((member) => (
            <tr key={member.id}>
              <td>name={member.firstName}</td>
              <td>surname={member.lastName}</td>
              <td>title={member.title}</td>
              <td>leavingStartDate={member.leavingStartDate}</td>
              <td>leavingEndDate={member.leavingEndDate}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      Member.renderMembersTable(this.state.members)
    );

    return (
      <div>
        <h1 id="tabelLabel">Members</h1>
        <p>This component created by Admin.</p>
        {contents}
      </div>
    );
  }
  async populateMemberData() {
    const response = await fetch("http://localhost:56644/home");
    const data = await response.json();
    this.setState({ members: data, loading: false });
  }
}
