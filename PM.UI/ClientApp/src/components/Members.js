import React, { Component } from "react";

export class Members extends Component {
    static displayName = "Permit Management";

    constructor(props) {
        super(props);
        this.state = { members: [], loading: true };
        // this.distrubuteLeaves = this.distrubuteLeaves.bind(this);
        // this.handleClick = this.handleClick.bind(this);
    }

    componentDidMount() {
        this.populateMembersData();
    }


    static handleClick() {
        console.log("here");

        fetch("http://localhost:56644/permit/GetDisributePermits", {
            method: "POST",
        });
    }

    static renderMembersTable(members) {
        //debugger;
        return (
            <div>
                <button className="btn btn-primary" onClick={this.handleClick}>
                    Distribute
        </button>
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
                                <td>{member.name}</td>
                                <td>{member.lastName}</td>
                                <td>{member.title}</td>
                                <td>{member.leavingStartDate}</td>
                                <td>{member.leavingEndDate}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading ? (
            <p>
                <em>Loading...</em>
            </p>
        ) : (
                Members.renderMembersTable(this.state.members)
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


//<tbody>
//    {members.map((member) => (
//        <tr key={member.id}>
//            <td>{member.name}</td>
//            <td>{member.lastName}</td>
//            <td>{member.title}</td>
//            <td>{member.leavingStartDate}</td>
//            <td>{moment(new Date(member.leavingStartDate)).format("DD-mm-yyyy")}</td>
//            <td>{member.leavingEndDate}</td>
//        </tr>
//    ))}
//</tbody>