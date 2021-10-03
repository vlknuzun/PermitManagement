import React from "react";
import MemberHeaderComponent from "./MemberHeaderComponent";
import MemberItemComponent from "./MemberItemComponent";

const MemberListComponent = (props) => {
  return (
    <table className="table table-striped" aria-labelledby="tabelLabel">
      <thead>
        <MemberHeaderComponent />
      </thead>
      <tbody>
        {props.members.map((member) => (
          <MemberItemComponent member={member} />
        ))}
      </tbody>
    </table>
  );
};

export default MemberListComponent;

 