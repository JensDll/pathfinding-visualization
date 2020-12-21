import { onBeforeUnmount } from 'vue';

export function useDraggable(element: HTMLElement, margin = 0) {
  let x = 0;
  let y = 0;

  const onDrag = (e: MouseEvent) => {
    e.preventDefault();
    element.style.top = `${element.offsetTop + e.clientY - y - margin}px`;
    element.style.left = `${element.offsetLeft + e.clientX - x - margin}px`;
    x = e.clientX;
    y = e.clientY;
  };

  const removeListeners = () => {
    document.removeEventListener('mousemove', onDrag);
    document.removeEventListener('mouseup', removeListeners);
  };

  const onMouseDown = (e: MouseEvent) => {
    e.preventDefault();
    x = e.clientX;
    y = e.clientY;

    document.addEventListener('mousemove', onDrag);
    document.addEventListener('mouseup', removeListeners);
  };

  onBeforeUnmount(() => {
    removeListeners();
  });

  return {
    onMouseDown
  };
}
