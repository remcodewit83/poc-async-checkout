function HeaderItem({ Icon }) {
  return (
    <div className="flex flex-col items-center cursor-pointer w-12 sm:w-20 group">
      <Icon className="h-8 mb-1" />
    </div>
  );
}

export default HeaderItem;
