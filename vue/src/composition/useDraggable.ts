import { onBeforeUnmount, onMounted, Ref, ref } from 'vue';

export function useDraggable(templateRef: Ref<HTMLElement>) {
  const isDraggable = ref(false);
  const dragging = ref(false);

  onMounted(() => {
    const element = templateRef.value;

    let initialX = 0;
    let initialY = 0;
    let dx = 0;
    let dy = 0;

    const onDrag = (e: MouseEvent) => {
      e.preventDefault();

      dx = e.clientX - initialX;
      dy = e.clientY - initialY;

      element.style.transform = `translate(${dx}px,${dy}px)`;
    };

    const removeListeners = () => {
      dragging.value = false;
      document.removeEventListener('mousemove', onDrag);
      document.removeEventListener('mouseup', removeListeners);
    };

    const onMouseDown = (e: MouseEvent) => {
      e.preventDefault();

      initialX = e.clientX - dx;
      initialY = e.clientY - dy;

      document.addEventListener('mousemove', onDrag);
      document.addEventListener('mouseup', removeListeners);
    };

    element.onmousedown = e => {
      if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
        dragging.value = true;
        onMouseDown(e);
      }
    };

    element.onmouseenter = () => {
      element.focus();
    };

    element.onmouseleave = () => {
      element.blur();
      isDraggable.value = false;
    };

    element.onkeydown = e => {
      if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
        isDraggable.value = true;
      }
    };

    element.onkeyup = () => {
      isDraggable.value = false;
    };

    onBeforeUnmount(() => {
      removeListeners();
    });
  });

  return {
    isDraggable,
    dragging
  };
}
