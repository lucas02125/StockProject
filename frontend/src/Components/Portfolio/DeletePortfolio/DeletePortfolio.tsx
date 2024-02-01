import React, { SyntheticEvent } from "react";

interface Props {
  OnRemovePortfolio: (e: SyntheticEvent) => void;
  portfolioValue: string;
}

const DeletePortfolio = ({ OnRemovePortfolio, portfolioValue }: Props) => {
  return (
    <div>
      <form onSubmit={OnRemovePortfolio}>
        <input hidden={true} value={portfolioValue} />
        <button>X</button>
      </form>
    </div>
  );
};

export default DeletePortfolio;
